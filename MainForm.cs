using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PjsipDialer
{
    public partial class MainForm : Form
    {
        public static Endpoint ep;
        private Account defaultAcc;
        private PjsipAccount checkedAccount = null;
        private PjsipCall currentCall = null;
        private const string cfgFileName = "pjsipdialer.xml";
        private CallButtonState currentState = CallButtonState.Disable;
        private PjsipRing ring;

        /// <summary>
        /// Инициализация PJSIP
        /// </summary>
        private void PjsipInit()
        {
            try
            {
                ep = new Endpoint();
                ep.libCreate();

                EpConfig epConfig = new EpConfig();
                ep.libInit(epConfig);

                TransportConfig tcfg = new TransportConfig();
                tcfg.port = 5080;
                ep.transportCreate(pjsip_transport_type_e.PJSIP_TRANSPORT_UDP, tcfg);
                ep.transportCreate(pjsip_transport_type_e.PJSIP_TRANSPORT_TCP, tcfg);

                ep.libStart();

                AccountConfig accCfg = new AccountConfig();
                accCfg.idUri = "sip:default@pjsip.org";

                defaultAcc = new Account();
                defaultAcc.create(accCfg, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка инициализации PJSIP\r\n{0}", ex.Message));
                this.Close();
            }
        }

        /// <summary>
        /// Остановка PJSIP и освобождение ресурсов
        /// </summary>
        private void PjsipClose()
        {
            try
            {
                defaultAcc.shutdown();
                defaultAcc.Dispose();
                PjsipAccount[] accounts = GetAccounts();
                foreach (PjsipAccount acc in accounts)
                {
                    acc.shutdown();
                    acc.Dispose();
                }

                ep.libDestroy();
                ep.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// Обрабатываем изменение статуса аккаунтов
        /// </summary>
        /// <param name="acc"></param>
        private void UpdateStatus(PjsipAccount acc)
        {
            if (InvokeRequired)
            {
                AccountRegState ars = new AccountRegState(UpdateStatus);
                Invoke(ars, new object[] { acc });
            }
            else
            {
                int index = lbAccounts.Items.IndexOf(acc);
                lbAccounts.Invalidate(lbAccounts.GetItemRectangle(index));
            }
        }

        /// <summary>
        /// Добавление нового аккаунта
        /// </summary>
        private void AccAdd()
        {
            AccountForm af = new AccountForm();
            af.Account = new PjsipAccount();
            if (af.ShowDialog() == DialogResult.OK)
            {
                PjsipAccount acc = af.Account;
                acc.OnAccountRegState = UpdateStatus;
                acc.OnAccountIncomingCall = IncomingCall;
                lbAccounts.Items.Add(acc);
                SaveConfig();
            }
            else af.Account.Dispose();
        }

        /// <summary>
        /// Редактирование аккаунта
        /// </summary>
        private void AccEdit()
        {
            PjsipAccount acc = lbAccounts.SelectedItem as PjsipAccount;
            if (acc != null)
            {
                AccountForm af = new AccountForm();
                af.Account = acc;
                if (af.ShowDialog() == DialogResult.OK)
                {
                    SaveConfig();
                }
            }
        }

        /// <summary>
        /// Удаление аккаунта
        /// </summary>
        private void AccDel()
        {
            PjsipAccount acc = lbAccounts.SelectedItem as PjsipAccount;
            if (acc != null && MessageBox.Show(this, string.Format("Аккаунт {0} будет удален, Вы уверены?", acc), "Удаление аккаунта", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lbAccounts.Items.Remove(acc);
                acc.Dispose();
                SaveConfig();
            }
        }

        /// <summary>
        /// Получение массива аккаунтов
        /// </summary>
        /// <returns></returns>
        private PjsipAccount[] GetAccounts()
        {
            List<PjsipAccount> accounts = new List<PjsipAccount>();
            for (int i = 0; i < lbAccounts.Items.Count; i++)
            {
                PjsipAccount acc = lbAccounts.Items[i] as PjsipAccount;
                accounts.Add(acc);
            }
            return accounts.ToArray();
        }

        /// <summary>
        /// Установка активного аккаунта
        /// </summary>
        /// <param name="acc"></param>
        private void SelectAccount(PjsipAccount acc)
        {
            int index = lbAccounts.Items.IndexOf(acc);
            if (index != -1)
            {
                lbAccounts.SetSelected(index, true);
                lbAccounts.SetItemChecked(index, true);
            }
        }

        /// <summary>
        /// Получение пути и имени файла конфигурации
        /// </summary>
        /// <returns></returns>
        private string GetConfigPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return path + "\\" + cfgFileName;
        }

        /// <summary>
        /// Сохранение конфигурации
        /// </summary>
        private void SaveConfig()
        {
            string fileName = GetConfigPath();
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PjsipConfig));
                TextWriter writer = new StreamWriter(fileName);

                PjsipConfig cfg = new PjsipConfig();
                cfg.Accounts = GetAccounts();

                serializer.Serialize(writer, cfg);
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка записи файла конфигурации\r\n{0}", ex.Message));
            }
        }

        /// <summary>
        /// Загрузка конфигурации
        /// </summary>
        private void LoadConfig()
        {
            string fileName = GetConfigPath();
            if (!File.Exists(fileName)) return;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PjsipConfig));
                FileStream fs = new FileStream(fileName, FileMode.Open);
                PjsipConfig cfg = (PjsipConfig)serializer.Deserialize(fs);
                fs.Close();
                if (cfg != null)
                {
                    foreach (PjsipAccount acc in cfg.Accounts)
                    {
                        acc.SetConfig();
                        acc.OnAccountRegState = UpdateStatus;
                        acc.OnAccountIncomingCall = IncomingCall;
                        lbAccounts.Items.Add(acc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка чтения файла конфигурации\r\n{0}", ex.Message));
            }
        }

        /// <summary>
        /// Установка активности кнопок редактирования и удаления аккаунтов
        /// </summary>
        private void CheckEnableButtons()
        {
            PjsipAccount acc = lbAccounts.SelectedItem as PjsipAccount;
            miDelete.Enabled = acc != null;
            miEdit.Enabled = acc != null;
        }

        /// <summary>
        /// Установка видимости наборной панели
        /// </summary>
        /// <param name="visible"></param>
        private void SetNumPadVisible(bool visible)
        {
            if (pNumPad.Visible == visible) return;
            int height = pNumPad.Height;
            pNumPad.Visible = visible;
            if (visible) this.Height += height;
            else this.Height -= height;
            lbCheckedAccount.Text = checkedAccount != null ? 
                checkedAccount.UserName + "@" + checkedAccount.Host : "";
        }

        private enum CallButtonState
        {
            Disable,
            Call,
            Hungup,
            Ring
        }

        /// <summary>
        /// Установка статуса кнопки вызова
        /// </summary>
        /// <param name="state"></param>
        private void SetCallButtonState(CallButtonState state)
        {
            switch (state)
            {
                case CallButtonState.Disable:
                    btCall.Enabled = false;
                    btCall.Image = imageList.Images[0];
                    break;
                case CallButtonState.Call:
                    btCall.Enabled = true;
                    btCall.Image = imageList.Images[1];
                    break;
                case CallButtonState.Hungup:
                    btCall.Enabled = true;
                    btCall.Image = imageList.Images[2];
                    break;
                case CallButtonState.Ring:
                    btCall.Enabled = true;
                    btCall.Image = imageList.Images[1];
                    break;
            }
            currentState = state;
        }

        /// <summary>
        /// Вызов на набранный номер
        /// </summary>
        private void MakeCall()
        {
            try
            {
                CallOpParam prm = new CallOpParam(true);
                currentCall = new PjsipCall(checkedAccount);
                string destUri = string.Format("sip:{0}@{1}", tbNumber.Text.Trim(), checkedAccount.Host);
                currentCall.makeCall(destUri, prm);
                currentCall.OnCallDisconnected = CallDisconnect;
                SetCallButtonState(CallButtonState.Hungup);
            }
            catch
            {
                Hungup();
            }
        }

        /// <summary>
        /// Положить трубку
        /// </summary>
        private void Hungup()
        {
            if (currentCall != null)
            {
                if (currentCall.IsOnline)
                {
                    try
                    {
                        CallOpParam prm = new CallOpParam();
                        prm.statusCode = pjsip_status_code.PJSIP_SC_GONE;
                        currentCall.hangup(prm);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Обработка разрыва соединения
        /// </summary>
        /// <param name="call"></param>
        private void CallDisconnect(PjsipCall call)
        {
            if (InvokeRequired)
            {
                CallDisconnected cd = new CallDisconnected(CallDisconnect);
                Invoke(cd, new object[] { call });
            }
            else
            {
                currentCall.Dispose();
                currentCall = null;
                SetCallButtonState(string.IsNullOrWhiteSpace(tbNumber.Text) ?
                    CallButtonState.Disable : CallButtonState.Call);
                if (ring != null)
                {
                    ring.Stop = true;
                    ring = null;
                }
            }
        }

        /// <summary>
        /// Обработка входящего вызова
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="callId"></param>
        private void IncomingCall(PjsipAccount acc, PjsipCall call, int callId)
        {
            if (InvokeRequired)
            {
                AccountIncomingCall ic = new AccountIncomingCall(IncomingCall);
                Invoke(ic, new object[] { acc, call, callId });
            }
            else
            {
                try
                {
                    if (currentCall != null && currentCall.IsOnline)
                    {
                        CallOpParam prm = new CallOpParam();
                        prm.statusCode = pjsip_status_code.PJSIP_SC_BUSY_HERE;
                        call.answer(prm);
                        call.Dispose();
                    }
                    else
                    {
                        SelectAccount(acc);
                        currentCall = call;
                        currentCall.OnCallIncoming = CallIncomingInfo;
                        currentCall.OnCallDisconnected = CallDisconnect;
                        SetCallButtonState(CallButtonState.Ring);
                        ring = new PjsipRing();
                    }
                }
                catch
                {
                    if (currentCall != null) Hungup();
                }
            }
        }

        /// <summary>
        /// Обработка определения номера входящего вызова
        /// </summary>
        /// <param name="remoteUri"></param>
        private void CallIncomingInfo(string remoteUri)
        {
            if (InvokeRequired)
            {
                CallIncoming ic = new CallIncoming(CallIncomingInfo);
                Invoke(ic, new object[] { remoteUri });
            }
            else
            {
                tbNumber.Text = NormalizeNumber(remoteUri);
            }
        }

        /// <summary>
        /// Нормализация номера входящего звонка
        /// </summary>
        /// <param name="remoteUri"></param>
        /// <returns></returns>
        private string NormalizeNumber(string remoteUri)
        {
            string result = "Anonymous";
            if (!string.IsNullOrEmpty(remoteUri))
            {
                int st = remoteUri.IndexOf("<sip:");
                int en = remoteUri.IndexOf("@");
                if (st != -1 && en != -1)
                {
                    st += 5;
                    result = remoteUri.Substring(st, en - st).Trim();
                    if (result.Length == 10) result = "8" + result;
                }
                else result = "Unknown";
            }
            return result;
        }

        /// <summary>
        /// Ответ на входящий вызов
        /// </summary>
        private void Answer()
        {
            if (ring != null)
            {
                ring.Stop = true;
                ring = null;
            }
            if (currentCall != null)
            {
                CallOpParam prm = new CallOpParam();
                prm.statusCode = pjsip_status_code.PJSIP_SC_OK;
                currentCall.answer(prm);
                SetCallButtonState(CallButtonState.Hungup);
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            SetNumPadVisible(false);
            PjsipInit();
            LoadConfig();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            PjsipClose();
        }

        private void btAccAdd_Click(object sender, EventArgs e)
        {
            AccAdd();
        }

        private void btAccEdit_Click(object sender, EventArgs e)
        {
            AccEdit();
        }

        private void btAccDel_Click(object sender, EventArgs e)
        {
            AccDel();
            CheckEnableButtons();
        }

        private void lbAccounts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < lbAccounts.Items.Count; i++)
                {
                    if (lbAccounts.GetItemChecked(i)) lbAccounts.SetItemChecked(i, false);
                }
                PjsipAccount acc = lbAccounts.Items[e.Index] as PjsipAccount;
                checkedAccount = null;
                if (acc != null)
                {
                    if (!acc.IsActive) e.NewValue = CheckState.Unchecked;
                    else checkedAccount = acc;
                }
                else e.NewValue = CheckState.Unchecked;
            }
            SetNumPadVisible(checkedAccount != null && e.NewValue == CheckState.Checked);
        }

        private void lbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckEnableButtons();
        }

        private void tbNumber_TextChanged(object sender, EventArgs e)
        {
            if (currentState == CallButtonState.Call || currentState == CallButtonState.Disable)
            {
                if (!string.IsNullOrWhiteSpace(tbNumber.Text))
                {
                    if (checkedAccount != null)
                    {
                        SetCallButtonState(checkedAccount.GetOnlineStatus() ?
                            CallButtonState.Hungup : CallButtonState.Call);
                    }
                }
                else if (checkedAccount != null && !checkedAccount.GetOnlineStatus())
                    SetCallButtonState(CallButtonState.Disable);
            }
        }

        private void Backspace()
        {
            string num = tbNumber.Text;
            if (num.Length == 0) return;
            tbNumber.Text = num.Substring(0, num.Length - 1);
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            Backspace();
        }

        private void btNum_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            tbNumber.Text += bt.Text;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (checkedAccount != null) {
                int kv = e.KeyValue;
                if (kv == 8) Backspace();
                else if (kv > 64 && kv < 91) tbNumber.Text += (char)kv;
                else if (kv == 51 && e.Shift) tbNumber.Text += "#";
                else if (kv == 106 || (kv == 56 && e.Shift)) tbNumber.Text += "*";
                else if (kv == 107 || (kv == 187 && e.Shift)) tbNumber.Text += "+";
                else if (kv > 95 && kv < 106) tbNumber.Text += (kv - 96).ToString();
                else if (kv > 47 && kv < 58) tbNumber.Text += (kv - 48).ToString();
                else if (kv == 13)
                {
                    if (currentState == CallButtonState.Call &&
                        !string.IsNullOrWhiteSpace(tbNumber.Text)) MakeCall();
                }
            }
        }

        private void btCall_Click(object sender, EventArgs e)
        {
            if (currentState == CallButtonState.Call) MakeCall();
            else if (currentState == CallButtonState.Hungup) Hungup();
            else if (currentState == CallButtonState.Ring) Answer();
        }
    }
}

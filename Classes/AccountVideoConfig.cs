//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.1
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class AccountVideoConfig : PersistentObject {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal AccountVideoConfig(global::System.IntPtr cPtr, bool cMemoryOwn) : base(pjsua2PINVOKE.AccountVideoConfig_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(AccountVideoConfig obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          pjsua2PINVOKE.delete_AccountVideoConfig(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public bool autoShowIncoming {
    set {
      pjsua2PINVOKE.AccountVideoConfig_autoShowIncoming_set(swigCPtr, value);
    } 
    get {
      bool ret = pjsua2PINVOKE.AccountVideoConfig_autoShowIncoming_get(swigCPtr);
      return ret;
    } 
  }

  public bool autoTransmitOutgoing {
    set {
      pjsua2PINVOKE.AccountVideoConfig_autoTransmitOutgoing_set(swigCPtr, value);
    } 
    get {
      bool ret = pjsua2PINVOKE.AccountVideoConfig_autoTransmitOutgoing_get(swigCPtr);
      return ret;
    } 
  }

  public uint windowFlags {
    set {
      pjsua2PINVOKE.AccountVideoConfig_windowFlags_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.AccountVideoConfig_windowFlags_get(swigCPtr);
      return ret;
    } 
  }

  public int defaultCaptureDevice {
    set {
      pjsua2PINVOKE.AccountVideoConfig_defaultCaptureDevice_set(swigCPtr, value);
    } 
    get {
      int ret = pjsua2PINVOKE.AccountVideoConfig_defaultCaptureDevice_get(swigCPtr);
      return ret;
    } 
  }

  public int defaultRenderDevice {
    set {
      pjsua2PINVOKE.AccountVideoConfig_defaultRenderDevice_set(swigCPtr, value);
    } 
    get {
      int ret = pjsua2PINVOKE.AccountVideoConfig_defaultRenderDevice_get(swigCPtr);
      return ret;
    } 
  }

  public pjmedia_vid_stream_rc_method rateControlMethod {
    set {
      pjsua2PINVOKE.AccountVideoConfig_rateControlMethod_set(swigCPtr, (int)value);
    } 
    get {
      pjmedia_vid_stream_rc_method ret = (pjmedia_vid_stream_rc_method)pjsua2PINVOKE.AccountVideoConfig_rateControlMethod_get(swigCPtr);
      return ret;
    } 
  }

  public uint rateControlBandwidth {
    set {
      pjsua2PINVOKE.AccountVideoConfig_rateControlBandwidth_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.AccountVideoConfig_rateControlBandwidth_get(swigCPtr);
      return ret;
    } 
  }

  public uint startKeyframeCount {
    set {
      pjsua2PINVOKE.AccountVideoConfig_startKeyframeCount_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.AccountVideoConfig_startKeyframeCount_get(swigCPtr);
      return ret;
    } 
  }

  public uint startKeyframeInterval {
    set {
      pjsua2PINVOKE.AccountVideoConfig_startKeyframeInterval_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.AccountVideoConfig_startKeyframeInterval_get(swigCPtr);
      return ret;
    } 
  }

  public override void readObject(ContainerNode node) {
    pjsua2PINVOKE.AccountVideoConfig_readObject(swigCPtr, ContainerNode.getCPtr(node));
    if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
  }

  public override void writeObject(ContainerNode node) {
    pjsua2PINVOKE.AccountVideoConfig_writeObject(swigCPtr, ContainerNode.getCPtr(node));
    if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
  }

  public AccountVideoConfig() : this(pjsua2PINVOKE.new_AccountVideoConfig(), true) {
  }

}

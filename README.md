# PjsipDialer
Тестовый проект SIP клиента на основе PJSIP библиотеки

Разработан в качестве пробы пера работы с нативной библиотекой PJSIP в СSharp

Среда разработки VisualStudio 2015, использован Microsoft .NET Framework 4.7

Включены возможности
1. Создание и сохранение аккаунтов регистрации
2. Инициация исходящего вызова
3. Прием входящего вызова

В папке Classes лежит SWIG обертка PJSUA2 библиотеки.

Для получения работоспособного приложения необходимо скачать последнюю версию опенсурс проекта PJSIP https://pjsip.org/

Открыть pjproject-vs14.sln в VisualStudio и создать пустой проект C++

Добавить в проект файлы 
1. /pjsip-apps/src/swig/csharp/pjsua2_wrap.h
2. /pjsip-apps/src/swig/csharp/pjsua2_wrap.cpp
3. /pjsip-apps/src/swig/pjsua2.i

Настроить свойства проекта
1. Target Name = PJSUA2
2. Target Extension = .dll
3. Configuration Type = Dynamic Library (.dll)
4. Common Language Runtime Support = Common Language Runtime Support (/clr)

И собрать проект

Целью является динамическая библиотека pjsua2.dll

Этот файл необходимо поместить рядом с PjsipDialer.exe, в процессе отладки это будет папка /bin/Debug

В проекте использовалась версия PJSIP 2.9

Если версия будет новее, то желательно обновить SWIG оболочку в папке Classes

Для этого необходимо установить SWIG http://www.swig.org/download.html

В версии для Windows есть исполняемый файл swig.exe, к нему необходимо прописать путь в системную переменную PATH

В командной строке VisualStudio перейти в каталог /pjsip-apps/src/swig/csharp и выполнить команду

swig -I../../../../pjlib/include -I../../../../pjlib-util/include -I../../../../pjmedia/include -I../../../../pjsip/include -I../../../../pjnath/include -w312 -c++ -csharp -o pjsua2_wrap.cpp ../pjsua2.i

В папке /pjsip-apps/src/swig/csharp будут созданы необходимые файлы, ими нужно заменить содержимое папки Classes

Удачи!

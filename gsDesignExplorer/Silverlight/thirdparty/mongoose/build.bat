set MINGWDBG=-DNDEBUG -Os
set MINGWOPT=-W -Wall -mthreads -Wl,--subsystem,windows %MINGWDBG%

gcc %MINGWOPT% mongoose.c -lws2_32 -shared -Wl,--out-implib=mongoose.lib -o _mongoose.dll
gcc %MINGWOPT% mongoose.c main.c win32\res.o -lws2_32 -ladvapi32 -o mongoose.exe

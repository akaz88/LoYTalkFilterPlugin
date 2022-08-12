@echo off
@setlocal enabledelayedexpansion

@set libs=-lib:..\core\,"..\..\Labyrinth of Yomi_Data\Managed\\"
@set dlls=-r:BepInEx.dll -r:0Harmony.dll -r:Assembly-CSharp.dll -r:UnityEngine.dll -r:UnityEngine.CoreModule.dll
@set out="LoY.Osaka.Plugin.dll"
@set csc="C:\Program Files\Mono\bin\mono.exe" %MONO_OPTIONS% "C:\Program Files\Mono\lib\mono\4.5\mcs.exe"
@set miscopt=-nologo -errorendlocation -langversion:7
@set compile=%csc% %flags% %miscopt% %libs% %dlls%

@echo compiling plugin...
%compile% -t:library -out:%out% LoY.Osaka.*.cs
if not %ERRORLEVEL% == 0 exit /b
move /Y %out% ..\plugins\ >nul
@echo done

rem  %compile% -t:exe test.cs LoY.Osaka.Translator.cs
rem  if not %ERRORLEVEL% == 0 exit /b
rem  test.exe
rem  exit /b

rem  xcopy .\Save\ "C:%HOMEPATH%\AppData\LocalLow\Aksys Games\Undernauts_ Labyrinth of Yomi\Save" /h /r /y /i /e /f >nul
exit /b
cd "E:\game\steamapps\common\Undernauts Labyrinth of Yomi\"
"Labyrinth of Yomi.exe" -screen-height 720 -screen-width 1280 -windows-mode

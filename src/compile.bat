rem  BepInEx\src\となるフォルダ構成にして実行して下さい

@echo off
@setlocal enabledelayedexpansion

@set libs=-lib:..\core\,..\plugins\,..\plugins\LoY.TalkFilter.Plugin\,"..\..\Labyrinth of Yomi_Data\Managed\\"
@set dlls=-r:BepInEx.dll -r:0Harmony.dll -r:Assembly-CSharp.dll -r:UnityEngine.dll -r:UnityEngine.CoreModule.dll -r:LibNMeCab.dll -r:LoY.Util.Plugin.dll
@set out="LoY.TalkFilter.Plugin.dll"
@set csc="C:\Program Files\Mono\bin\mono.exe" %MONO_OPTIONS% "C:\Program Files\Mono\lib\mono\4.5\mcs.exe"
@set miscopt=-nologo -errorendlocation -langversion:7
@set compile=%csc% %flags% %miscopt% %libs% %dlls%

@echo compiling plugin...
%compile% -t:library -out:%out% src\LoY.TalkFilter.*.cs
if not %ERRORLEVEL% == 0 exit /b
move /Y %out% ..\plugins\ >nul
@echo done

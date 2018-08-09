#include <Constants.au3>
#include <GuiButton.au3>
#include <GuiImageList.au3>
#include <GUIConstantsEx.au3>
#include <WindowsConstants.au3>
Opt("TrayMenuMode", 1)
Opt("TrayOnEventMode", 1)

Local $aParts[3] = [320, 460, 600], $hWndb, $sTafa = True, $sTafb = True
FileInstall("D:\TDDOWNLOAD\Reg-icos\BG.ico", @TempDir & "\BG.ico", 1)
FileInstall("D:\TDDOWNLOAD\Reg-icos\FDJ.ico", @TempDir & "\FDJ.ico", 1)
FileInstall("D:\TDDOWNLOAD\正则表达式帮助文档.chm", @TempDir & "\正则表达式帮助文档.chm", 1)

$Form1 = GUICreate("正则表达式测试工具V4.0        By：水木子", 600, 500)
TraySetOnEvent($TRAY_EVENT_PRIMARYUP, "SpecialEvent")

$Tab1 = GUICtrlCreateTab(2, 2, 596, 475)
GUICtrlSetFont(-1, 10, 400, 0, "Arial")
GUICtrlSetResizing(-1, $GUI_DOCKWIDTH + $GUI_DOCKHEIGHT)

$TabSheet1 = GUICtrlCreateTabItem("匹配模式")
GUICtrlSetImage(-1, @TempDir & '\FDJ.ico')
GUICtrlCreateLabel("需要匹配的字符串：", 10, 40, 110, 17)
$Edit01 = GUICtrlCreateEdit("", 10, 60, 580, 180)
GUICtrlSetLimit(-1, 999999999)

GUICtrlCreateLabel("表达式：", 10, 263, 50, 17)
$Input01 = GUICtrlCreateInput("", 60, 260, 400, 21)
$But01 = GUICtrlCreateButton("匹配", 460, 258, 60, 25, $WS_GROUP)
_SetIcon(-1, @TempDir & '\FDJ.ico')

$But02 = GUICtrlCreateButton("清空", 530, 258, 60, 25, $WS_GROUP)
GUICtrlSetTip(-1, '清空当前模式下所有控件中的内容。')
_SetIcon(-1, 'shell32.dll', 32)

GUICtrlCreateLabel("匹配结果：", 10, 300, 60, 17)
$Edit02 = GUICtrlCreateEdit("", 10, 320, 580, 150)
GUICtrlSetLimit(-1, 999999999)

$TabSheet2 = GUICtrlCreateTabItem("替换模式")
GUICtrlSetImage(-1, @TempDir & '\BG.ico')
GUICtrlCreateLabel("需要替换的字符串：", 10, 40, 110, 17)
$Edit11 = GUICtrlCreateEdit("", 10, 60, 580, 180)
GUICtrlSetLimit(-1, 999999999)

GUICtrlCreateLabel("表达式：", 22, 253, 50, 17)
$Input11 = GUICtrlCreateInput("", 70, 250, 400, 21)
$But11 = GUICtrlCreateButton("替换", 470, 248, 60, 25, $WS_GROUP)
_SetIcon(-1, @TempDir & '\BG.ico')

$But12 = GUICtrlCreateButton("清空", 530, 248, 60, 25, $WS_GROUP)
GUICtrlSetTip(-1, '清空当前模式下所有控件中的内容。')
_SetIcon(-1, 'shell32.dll', 32)

GUICtrlCreateLabel("替换内容：", 10, 283, 60, 17)
$Input12 = GUICtrlCreateInput("", 70, 280, 400, 21)

GUICtrlCreateLabel("替换次数：", 480, 283, 60, 17)
$Input13 = GUICtrlCreateCombo("", 540, 280, 50, 21)
GUICtrlSetData(-1, "0|1|2|3|4|5|6|7|8|9|10", '0')
GUICtrlSetTip(-1, '匹配次数：需要执行替换的次数。' & @CRLF & '默认为 0 使用 0 为全部替换。')

GUICtrlCreateLabel("替换结果：", 10, 305, 60, 17)
$Edit12 = GUICtrlCreateEdit("", 10, 320, 580, 150)
GUICtrlSetLimit(-1, 999999999)
GUICtrlCreateTabItem("")

GUICtrlCreateLabel("AutoIt3 中文论坛：", 340, 5, 110, 17)
$Label0 = GUICtrlCreateLabel("http://www.autoitx.com", 448, 5, 132, 17)
GUICtrlSetCursor(-1, 0)

GUICtrlCreateLabel("运行状态：", 5, 482, 250, 17)
$Label1 = GUICtrlCreateLabel("准备就绪", 65, 482, 250, 17)
$Label2 = GUICtrlCreateLabel("《正则表达式入门教程》", 310, 482, 132, 17)
GUICtrlSetCursor(-1, 0)
$Label3 = GUICtrlCreateLabel("《正则表达式帮助文档》", 460, 482, 132, 17)
GUICtrlSetCursor(-1, 0)
GUISetState(@SW_SHOW)

While 1
	$Pos = GUIGetCursorInfo($Form1)
	If Not @error Then _Hyperlink($Pos[4])

	$nMsg = GUIGetMsg()
	Switch $nMsg
		Case - 3
			Exit
		Case $But01
			Match(GUICtrlRead($Edit01), GUICtrlRead($Input01))
		Case $But11
			Replace(GUICtrlRead($Edit11), GUICtrlRead($Input11), GUICtrlRead($Input12), GUICtrlRead($Input13))
		Case $But02
			GUICtrlSetData($Edit01, '')
			GUICtrlSetData($Edit02, '')
			GUICtrlSetData($Input01, '')
			GUICtrlSetData($Label1, '准备就绪')
			GUICtrlSetColor($Label1, 0x000000)
			GUICtrlSetState($Edit01, $GUI_FOCUS)
		Case $But12
			GUICtrlSetData($Edit11, '')
			GUICtrlSetData($Edit12, '')
			GUICtrlSetData($Input11, '')
			GUICtrlSetData($Input12, '')
			GUICtrlSetData($Input13, 0)
			GUICtrlSetData($Label1, '准备就绪')
			GUICtrlSetColor($Label1, 0x000000)
			GUICtrlSetState($Edit11, $GUI_FOCUS)
		Case $Label0
			ShellExecute('http://www.AutoItx.com')
		Case $Label2
			ShellExecute('http://deerchao.net/tutorials/regex/regex.htm')
		Case $Label3
			ShellExecute(@TempDir & "\正则表达式帮助文档.chm")
	EndSwitch
WEnd

Func _Hyperlink($hWnda)
	If $hWnda = 27 Or $hWnda = 30 Or $hWnda = 31 Then
		If $sTafb = True Then
			GUICtrlSetFont($hWnda, 9, 400, 4)
			GUICtrlSetColor($hWnda, 0x0080CC)
			$hWndb = $hWnda
			$sTafb = False
			$sTafa = False
		EndIf
	Else
		If $sTafa = False Then
			GUICtrlSetFont($hWndb, 9)
			GUICtrlSetColor($hWndb, 0x000000)
			$sTafa = True
			$sTafb = True
		EndIf
	EndIf
EndFunc   ;==>_Hyperlink

Func Match($oString, $Expressions)
	Local $sResults
	If $oString <> '' And $Expressions <> '' Then
		GUICtrlSetData($Edit02, '')
		$sReg = StringRegExp($oString, $Expressions, 3)
		If UBound($sReg) = 0 Then
			GUICtrlSetData($Label1, '匹配失败，没有找到匹配的内容。')
			GUICtrlSetColor($Label1, 0xFF0000)
		Else
			For $i = 0 To UBound($sReg) - 1
				$sResults &= StringFormat("[%02d]", $i + 1) & $sReg[$i] & @CRLF
			Next
			GUICtrlSetData($Edit02, $sResults)
			GUICtrlSetData($Label1, '匹配成功，共找到' & UBound($sReg) & '组匹配的数据。')
			GUICtrlSetColor($Label1, 0x000000)
		EndIf
	Else
		GUICtrlSetData($Label1, '出错！请正确输入相关数据。')
		GUICtrlSetColor($Label1, 0xFF0000)
	EndIf
EndFunc   ;==>Match

Func Replace($oReplace, $Pattern, $Replace, $Count)
	If $oReplace <> '' And $Pattern <> '' Then
		GUICtrlSetData($Edit12, '')
		$Replace = StringRegExpReplace($oReplace, $Pattern, $Replace, $Count)
		If $Replace = $oReplace Then
			GUICtrlSetData($Label1, '替换失败，没有找到可以替换的内容。')
			GUICtrlSetColor($Label1, 0xFF0000)
		Else
			GUICtrlSetData($Edit12, $Replace)
			GUICtrlSetData($Label1, '替换成功，请查看替换结果。')
			GUICtrlSetColor($Label1, 0x000000)
		EndIf
	Else
		GUICtrlSetData($Label1, '出错！请正确输入相关数据。')
		GUICtrlSetColor($Label1, 0xFF0000)
	EndIf
EndFunc   ;==>Replace

Func SpecialEvent()
	If WinGetState($Form1) = 7 Then
		GUISetState(@SW_MINIMIZE)
	Else
		GUISetState(@SW_RESTORE)
	EndIf
EndFunc   ;==>SpecialEvent

Func _SetIcon($hWnda, $sFile, $iIndex = 0)
	$hImage1 = _GUIImageList_Create(20, 20, 5, 1, 0)
	_GUIImageList_AddIcon($hImage1, $sFile, $iIndex, True)
	_GUICtrlButton_SetImageList($hWnda, $hImage1)
EndFunc   ;==>_SetIcon
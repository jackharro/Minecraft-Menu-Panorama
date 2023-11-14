#Requires AutoHotkey v2.0

GroupAdd "Minecraft", "Minecraft"

#HotIf WinActive("ahk_group Minecraft")
!r::
{
SendCommand("tp @s ~ ~ ~ 0 0")
Sleep 50
Send "{F1}"
Sleep 50


; Take screenshots
loop 4 {
Send "{F2}"
Sleep 50
SendCommand("tp @s ~ ~ ~ ~90 0")
Sleep 50

}

loop 2 {
SendCommand("tp @s ~ ~ ~ 0 ~-180")
Sleep 50
Send "{F2}"
Sleep 50
}

SendCommand("tp @s ~ ~ ~ 0 0")
Send "{F1}"
SendCommand("title @s times 5 20 5")
SendCommand("title @s title `"done :)`"")
}

!L::
{
; Load world
SendCommand("title @s times 5 400 5")
SendCommand("title @s title `"Loading world…`"")
SendCommand("tp @s ~ ~ ~ 0 0")
Sleep 5000
loop 3 {
SendCommand("tp @s ~ ~ ~ ~90 0")
Sleep 5000
}
SendCommand("title @s times 5 20 5")
SendCommand("title @s title `"done :)`"")
}

SendCommand(c)
{
Send "{/}"
Sleep 60
SendText c
Sleep 100
Send "{Enter}"
}
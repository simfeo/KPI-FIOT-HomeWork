;
; lab2.asm
;
; Created: 1/16/2019 4:45:07 AM
; Author : sim
;


; Replace with your application code
main:

; sum of negative -1 -> -10
LDI R16, -1
LDI R17, -2
LDI R18, -10
LDI R19, -1

FLOOP:
ADD R16, R17
SUB R17, R19
CPSE R17, R18
RJMP FLOOP

; add 0x7Dh and 0x7eh
EOR R17, R17
EOR R18, R18
IN R17, HIGH(0X7D)
IN R18, HIGH(0X7E)

EOR R29, R29

ADD R29, R17
ADD R29, R18

ret

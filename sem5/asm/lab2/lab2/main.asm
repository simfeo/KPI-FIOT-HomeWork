;
; lab2.asm
;
; Created: 1/16/2019 4:45:07 AM
; Author : sim
;

rjmp main

EEPROM_read:
sbic EECR,EEWE
rjmp EEPROM_read

out EEARH, R18
out EEARL, R17
sbi EECR,EERE

in R16,EEDR
ret

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


;;;;

EOR R16, R16

ldi R17, low(0x60)
ldi R18, high(0x60)
rcall EEPROM_read

EOR R21, R21
add R21, R16

EOR R16, R16
ldi R17, low(0x63)
ldi R18, high(0x63)
rcall EEPROM_read
add R21, R16

;;;
EOR R16, R16

ldi R17, low(0x61)
ldi R18, high(0x61)
rcall EEPROM_read

EOR R22, R22
add R22, R16

EOR R16, R16
ldi R17, low(0x62)
ldi R18, high(0x62)
rcall EEPROM_read
add R22, R16



ret

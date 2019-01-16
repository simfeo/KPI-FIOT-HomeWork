;
; AssemblerApplication2.asm
;
; Created: 1/16/2019 4:21:59 AM
; Author : sim
;
; Replace with your application code

rjmp main

; EEPROM write function
EEPROM_write: 
sbic EECR, EEWE ; waite if eeprom not ready
rjmp EEPROM_write

out EEARH, R18 ; enable writing
out EEARL, r17 ; write address
out EEDR, r16 ; here is our data

; close eeprom write mod
sbi EECR, EEMWE 
sbi EECR, EEWE 
ret

EEPROM_read:
sbic EECR,EEWE
rjmp EEPROM_read

out EEARH, R18
out EEARL, R17
sbi EECR,EERE

in R16,EEDR
ret

; here where program runs
main:
;IN R16, UDR ; read UDR
IN R16, UDR


rcall EEPROM_write

; init stack 70h
LDI	R16, 0x70
OUT	SPL, R16


; read PIND
in R16, PIND
; write to stack
PUSH R16


; get 3 last items from 3x5 table starting from 60h
ldi R17, low(0x6A)
ldi R18, high(0x6A)
rcall EEPROM_read

ldi R17, low(0x80)
ldi R18, high(0x80)
rcall EEPROM_write
;;;;;
ldi R17, low(0x6B)
ldi R18, high(0x6B)
rcall EEPROM_read

ldi R17, low(0x81)
ldi R18, high(0x81)
rcall EEPROM_write
;;;;;;
ldi R17, low(0x6C)
ldi R18, high(0x6C)
rcall EEPROM_read

ldi R17, low(0x82)
ldi R18, high(0x82)
rcall EEPROM_write

ret


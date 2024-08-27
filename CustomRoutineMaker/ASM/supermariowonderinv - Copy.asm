//.swi

.org	0x002783c0-0x4000
bl	main


//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	0x02012cc0
main:
.word 	0xd000b0f6
ldr	x22,[x22,0xa98]
ldr	x22,[x22,0x20]
ldr	x22,[x22]
ldrh    w22,[x22,0x10]

ands	w22,w22,0x2000
bne	end
mov	w22,1
strh    w22,[x9,#0x38]
end:

ldrsh   w9, [x9,#0x38]
ret


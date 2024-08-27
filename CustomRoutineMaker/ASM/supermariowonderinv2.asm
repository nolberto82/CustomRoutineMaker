//.swi

.org	0x0087ec14-0x4000
bl	main


//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	0x02012c80
main:


.word 	0xd000b0e9
ldr	x9,[x9,0xa98]
ldr	x9,[x9,0x20]
ldr	x9,[x9]
ldrh    w9,[x9,0x10]
ands	w1,w9,0x2000

ldr	x8, [x21,#0x1a0]
ldr     w9, [x19,#0x680]
add	x8,x8,x9
mov	w1,1
csel	w1,wzr,w1,ne
store:
strb	w1,[x8]


ret


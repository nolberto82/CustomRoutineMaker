.nds
.create "out.bin", 0x00000000


.org	0x0201f078
bl	0x02000000


//ecode:
//.dw	 0xe2000000
//evalue:
//.dw	 0xe5903000

.org	0x02000000


ldr	r3,[r0,-0xf4]
ldr	r1,[r0,0x14]
ldr	r3,[r3]
str	r3,[r1]

ldr	r3,[r0]


bx	lr
.close

.nds
.create "out.bin", 0x00000000


.org	0x0208f24c
bl	0x02000030
.pool

//ecode:
//.dw	 0xe2000030
//evalue:
//.dw	 0x00000000

.org	0x02000030

mov	r7,0x7f00
lsl	r7,16
str	r7,[r13,-0x110]
str	r7,[r13,-0x108]
str	r7,[r13,-0x100]
str	r7,[r13,-0x0f8]
str	r7,[r10,0x138]
str	r7,[r10,0x148]
str	r7,[r10,0x14c]


ldr	r0,[r10,0x130]

bx	lr
.pool

//normal:
//ldr	r7,=0x021d7b30
//ldrh	r7,[r7]
//mov	r2,0x100
//ands	r2,r7
//beq	end2

//mov	r2,0x7f0000
//str	r2,[r0,0]

//end2:
//ldmia	[r0],r0-r2

//bx	lr
//.pool

//.org	0x02073a14
//bl	normal

.close

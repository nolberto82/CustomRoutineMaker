.nds
.create "out.bin", 0x02000000


.org	0x020695b8
bl	0x02000000


.org	0x02000000


add	r0,r6,0x400

ldr	r0,[r0,0x22]
ands	r0,r0,3
mov	r0,0xfa00
strne	r0,[r6,0x30]

ldr	r0,[r6,0x58]
bx	r14
.pool
.close

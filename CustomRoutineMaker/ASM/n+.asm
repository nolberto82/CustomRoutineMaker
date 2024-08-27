.nds
.create "out.bin", 0x02000000


.org	0x0201b648
bl	0x02000000


.org	0x02000000


ldr	r0,[r4,#0x140]
ldr	r0,[r0]
ldr	r0,[r0,#0x34]
ands	r0,r0,4
movne	r0,#0x2000
strne	r0,[r2]
mov	r0,r6


bx	r14
.pool
.close

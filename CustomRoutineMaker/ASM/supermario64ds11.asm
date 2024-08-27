.nds
.create "out.bin", 0x02000000


.org	0x020da04c
bl	0x02000000


.org	0x02000000


ldrh	r1,[r1,2]
ands	r1,r1,2
ldr	r1,=0x35cbc
strne	r1,[r4,0xa8]
ldr	r0,[r4,0x6e5]
bx	r14
.pool
.close

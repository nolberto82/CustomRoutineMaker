.nds
.create "out.bin", 0x02000000


.org	0x020da1bc
bl	0x02000000


.org	0x02000000


ldr 	r0,=0x2097598
ldrh	r0,[r0,2]
ands 	r0,r0,2
bx	r14
.pool
.close

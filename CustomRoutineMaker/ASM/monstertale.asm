.nds
.create "out.bin", 0x02000000


.org	0x0200b300
bl	0x02000000


.org	0x02000000


ldr	r3,=0x020e7588

ldrb	r1,[r3]
ands	r1,r1,#0x80
bne	end


ldrb	r1,[r3,#2]
ands	r1,r1,#2
ldr	r1,=0xffffb800
strne	r1,[r6,#0x20]

end:
ldr	r1,[r6,#0x20]


bx	r14
.pool
.close

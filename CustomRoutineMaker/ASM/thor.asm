.nds
.create "out.bin", 0x02000000


.org	0x020a6900
b	0x02000000


.org	0x02000000



ldr	r2,=0x020fb2d8
ldrb	r2,[r2]
ands	r2,r2,2
beq	end
mov	r2,0x6800
rsb	r2,r2,0
strne	r2,[r0,0x38]
end:
ldr	r2,[r0,0x38]


b	0x020a6904
.pool
.close

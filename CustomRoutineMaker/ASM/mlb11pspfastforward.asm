.psp
.create "out.bin", 0x08800000


.org	0x09a04710
j	0x08800f00
add	r0,r0


.org	0x08800f00



lhu	t0,0x0e(a0)
andi	t0,t0,8
beq	t0,zero,end
li	t0,0
andi	t0,t0,0xfff7
ori	t0,t0,0x0800
sh	t0,0x12(a0)
sh	t0,0x0e(a0)

end:


lhu	a0,0x12(a0)
andi	a0,a0,8

j	0x09a04718
.close

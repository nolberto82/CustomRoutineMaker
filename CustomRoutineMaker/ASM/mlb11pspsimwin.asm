.psp
.create "out.bin", 0x08800000


.org	0x097ec958
j	0x08801280

.org	0x08801280


lui	t0,0x99c
lw	t0,0x7f08(t0)
lbu	t1,0x4(a0) //home score
lbu	t2,0x1(a0) //away score
lbu	t3,0x2(a0) //home team id
lbu	t4,0x5(a0) //away team id
lbu	t5,0x43(t0) //user team id
beq	t4,t5,userhome
nop
beq	t3,t5,useraway
nop
b	end
nop
userhome:
slt	t0,t1,t2
bne	t0,zero,swap
nop
b 	end
nop
useraway:
slt	t0,t2,t1
beq	t0,zero,end
nop
swap:
sb	t2,0x4(a0)
sb	t1,0x1(a0)
end:
lbu	a1,0x4(a0)


j	0x097ec960
.close

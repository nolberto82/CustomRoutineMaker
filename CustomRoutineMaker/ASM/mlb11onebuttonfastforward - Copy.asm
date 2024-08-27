.psp
.create "out.bin", 0x08800000


.org	0x097e1554
j	0x08801400

add	r0,r0

.org	0x08801400


lhu	a1,0x12(a1)
andi	a1,a1,0x804
beq	a1,zero,end
li	t3,0x09b483b0
lw	t3,8(t3)
lw	t3,0x24(t3)
bne	t3,zero,end
nop
ori	a1,0x800
sh	a1,0x12(a0)
end:
j	0x097e155c

.org	0x0992f974
j	0x08801440
add	r0,r0

.org	0x08801440


lui	t3,0x9b5
lw	t3,-0x37c(t3)
lw	t3,(t3)
lhu	t3,0x12(t3)
andi	t3,t3,0x5000
bne	t3,zero,end2
lw	t0,0xb8(a0)
li	t3,0x08b6bf30
bne	t3,t0,end2
li	t3,5
lui	t0,0x08bf
sw	t3,0xcc50(t0)
li	t0,0x08b6be7c
sw	t0,0xb8(a0)
end2:
addu	a0,a2,a3
j	0x0992f97c

.org	0x09a57958
j	0x08801490

.org	0x08801490


lui	t0,0x9b7
lw	t0,0x16c4(t0)
lbu	t0,0x43(t0) //user team id
lbu	t1,0x2(s0) //home team id
lbu	t3,0xcc(s0) //inning half
beq	t3,zero,away
nop
beq	t1,t0,skip
nop
away:
bne	t3,zero,end3
lbu	t1,0x5(s0) //away team id
bne	t1,t0,end3
nop
skip:
sb	r0,0x78(s0)
end3:
lbu	a0,0x78(s0)
j	0x09a57960

.close

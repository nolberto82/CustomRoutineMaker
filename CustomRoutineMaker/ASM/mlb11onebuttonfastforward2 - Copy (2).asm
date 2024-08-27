.psp
.create "out.bin", 0x00000000


.org	0x097650bc
j	0x08801500

//ecode:
//.dd	0xe00f425c
//evalue:
//.dd	0x0110ffc8

.org	0x08801500

sb	zero,0x16c(a0)
sw	ra,-4(sp)
lui	a0,0x9b8
li	t0,0x09756364
jalr	t0
lw	a0,-0x514c(a0)
end:

lw	ra,-4(sp)
jr	ra
li	v0,0
	
.org	0x09766f44
add	r0,r0

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

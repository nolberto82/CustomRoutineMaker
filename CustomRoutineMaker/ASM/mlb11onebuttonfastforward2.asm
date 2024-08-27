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
lw	ra,-4(sp)
jr	ra
li	v0,0
	
.org	0x09766f44
add	r0,r0

.org	0x09a5664c
j	0x08801490

.org	0x08801490

//lui	t0,0x9b7
//lw	t0,0x16c4(t0)

li	t0,0x088d7f80
lbu	t1,0xcc(s0) //half inning
lbu	t2,0x3b(t0) //user home or away
beq	t1,t2,end
mtc1	r0,f20
mov.s	f12,f20
end:
c.lt.s	f12,f20
j	0x09a5664c+8

.close

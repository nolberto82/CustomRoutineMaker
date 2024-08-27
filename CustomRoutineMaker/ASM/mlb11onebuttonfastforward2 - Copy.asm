.psp
.create "out.bin", 0x00000000


.org	0x09a8691c
j	0x08801500

//ecode:
//.dd	0xe00f425c
//evalue:
//.dd	0x0110ffc8

.org	0x08801500


jalr	t0
nop

li	t2,0x09b483b0
lw	t2,0x8(t2)
lui	t1,0x9b5
lw	t1,-0x37c(t1)
lw	t1,(t1)
lhu	t1,0x12(t1)
andi	t1,t1,0x100
beq	t1,zero,end
nop
lui	a0,0x9b8
li	t0,0x09756364
jalr	t0
lw	a0,-0x514c(a0)
nop
end:
j	0x09a86924

//.org	0x09766f3c
//lhu	a2,0xe(a2)

//.org 0x097ea9e0
//add	r0,r0

.close

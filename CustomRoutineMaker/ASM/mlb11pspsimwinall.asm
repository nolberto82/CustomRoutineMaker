.psp
.create "out.bin", 0x08800000


.org	0x097ec964
j	0x08801700

//ecode:
//.dw	 0xe0040438
//evalue:
//.dw	 0x00f8fbc4

.org	0x08801700

lbu	t4,0x08a0ff6b

lbu	t2,0x1(a0) //away score

li	t5,0x3f //analogup
bne	t4,t5,home
slt	t0,a1,t2
beq	t0,zero,swap

home:
li	t5,0xbf //analog down
bne	t4,t5,end
slt	t0,a1,t2
beq	t0,zero,end

swap:
move	t3,a1
move	a1,t2
move	t2,t3

sb	a1,0x4(a0)
sb	t2,0x1(a0)
end:

addiu	a3,a3,0x3c

j	0x097ec964+8
.close

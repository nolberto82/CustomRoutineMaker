.psp
.create "out.bin", 0x00000000


.org	0x09912fdc
j	0x08801000

//ecode:
//.dd	0xe00f425c
//evalue:
//.dd	0x0110ffc8


.org	0x08801000


lui	t1,0x9b5
lh	t0,-0x7270(t1)
andi	t0,t0,0x10
beq	t0,zero,end
nop
//lb	t1,0x08800ffc
//li	t0,0x4280
//beq	t1,r0,lefty
//nop
//li	t0,0xc280

//lefty:
sw	r0,0x80(s1)
li	t0,0x4300
sh	t0,0x84+2(s1)
li	t0,0xc320
sh	t0,0x88+2(s1)
end:

lwc1	f12,0xbc(s1)
j	0x09912fdc+8

//.org	0x09813a10
//j	0x08800fc0

//.org	0x08800fc0

//sb	s2,0x08800ffc
//move	t2,a0
//j	0x09813a10+8
.close

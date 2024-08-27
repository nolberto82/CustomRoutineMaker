.psp
.create "out.bin", 0x00000000


.org	0x09913044
j	0x08801000

//ecode:
//.dd	0xe00009e1
//evalue:
//.dd	0x01113044


.org	0x08801000


lui	t0,0x9b5
lh	t0,-0x7270(t0)
andi 	t0,t0,0x10
beq	t0,zero,end

lui	t0,0x4300
sw	t0,0x84(s1)
lui	t0,0xc330
sw	t0,0x88(s1)
//sw	r0,0x80(s1)

end:
lui	a0,0x9e1
j	0x0991304c

.close

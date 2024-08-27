.psp
.create "out.bin", 0x08800000


.org	0x0958e630
j	0x08801100
add	r0,r0


.org	0x08801100



lhu	s0,0x0010(a0)
andi	s3,s0,0x100
beq	s3,zero,end
ori	s0,zero,0x0140
sh	s0,0x0012(a0)
end:

lhu	s0,0x0012(a0)
andi	s3,s0,0x4
j	0x0958e638

.org	0x0987711c
j	0x08801180


.org	0x08801180


sw	s3,0x1c8(sp)
lw	t2,0x70c4(fp)
lw	t2,(t2)
lhu	t2,0x12(t2)
andi	t2,t2,0x100
beq	t2,zero,end2
add	t2,s0,a1
lbu	a3,0x2bb5(s0)
li	t1,2
beq	a3,t1,end2
addiu	a3,a3,-1
loop:
add	t0,a3,a3
addu	t0,t2,t0
lhu	t0,0x454(t0)
bne	t0,t1,wrongoption
nop
sb	a3,0x2bb3(s0)
j	0x0987715c
move	a1,t2
wrongoption:
addiu	a3,a3,-1
bge	a3,zero,loop
nop
end2:
j	0x09877124
.close

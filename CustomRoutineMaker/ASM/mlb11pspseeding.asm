.psp
.create "out.bin", 0x08800000


.org	0x0978fb90
j	0x08801200


.org	0x08801200



addiu	s5,s5,-0x1


lw	s2,0x000c(a0)
addiu	a1,s2,-0x3e8
bltz	a1,next

lw	a1,0x0004(a0)
sw	s2,0x0004(a0)
sw	a1,0x000c(a0)

next:
lw	s2,0x001c(a0)
addiu	a1,s2,-0x3e8
bltz	a1,end

lw	a1,0x0014(a0)
sw	s2,0x0014(a0)
sw	a1,0x001c(a0)

end:
j	0x0978fb98
.close

.ps2
.create "out.bin", 0x0


.org	0x00827110
j	0x000a0000

.org	0x000a0000

la	a2,0x0021dd8e
lbu	v1,0x0001(a2)
bne	v1,zero,hr
lbu	v1,0x0003(a2)
beq	v1,zero,end
nop
hr:
lw	a2,0x80(s1)
lui	v1,0xf000
and	a2,v1
lui	v1,0x02aa
or	a2,v1	
sw	a2,0x80(s1)
lui	a2,0x42aa
sw	a2,0x84(s1)
lui	a2,0xc2ea
sw	a2,0x88(s1)

end:
lui	a2,0x008c
li	v1,-0x1
j	0x00827118
.close


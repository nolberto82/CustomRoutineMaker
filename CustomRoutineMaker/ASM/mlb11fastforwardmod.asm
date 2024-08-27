.psp
.create "out.bin", 0x08800000


.org	0x09930800
j	0x08801030


.org	0x08801030
sw	zero,0x0(sp)
la	a2,0x08b6bf30
bne	a2,a1,end
lui	a2,0x9b5
lh	a2,-0x7270(a2)
andi 	a2,a2,0x4000
beq	a2,zero,end
nop
la	a1,0x08b6be7c
end:
j	0x09930808
.close

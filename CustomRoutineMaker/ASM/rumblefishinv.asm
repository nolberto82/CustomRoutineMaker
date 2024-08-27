.ps2
.create "out.bin", 0x20000000


.org	0x2022a000
j	0x200a0000


.org	0x200a0000


lwc1	f01,(v1)
lw	a0,-0x6c28(gp)
lw	a0,0x4(a0)
bne	s4,a0,end
nop

j	0x2022a150
nop
end:
j	0x2022a008
.close

	.ps2
.create "out.bin", 0x20000000


.org	0x202295b8
j	0x200a0030


.org	0x200a0030


lw	s0,-0x6c28(gp)
lw	s0,0x4(s0)
lw	s7,0x50(sp)
beq	s0,s7,end
nop
li	v0,1
end:
j	0x20229918
.close
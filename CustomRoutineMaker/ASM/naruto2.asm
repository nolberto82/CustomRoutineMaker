.ps2
.create "out.bin", 0x20000000


.org	0x20172458
j	0x200a0000
nop


.org	0x200a0000



lw	v0,-0x7a58(gp)
bne	v0,s3,end
lbu	v0,0x2c0(a0)
j	0x00173508

nop
end:

j	0x20172460
dsll32	v0,0x1c
.close

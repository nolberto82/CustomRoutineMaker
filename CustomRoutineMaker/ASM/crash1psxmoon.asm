.psx
.create "out.bin", 0x80000000


.org	0x8001feec
j	0x800083a0


.org	0x800083a0

la	v0,0xc0000
sw	v0,0xa8(s2)
lui	v0,0x10
j	0x800201c0
.close
.psx
.create "out.bin", 0x80000000

.org	0x80023bb4
j	0x80008300

.org	0x80008300

la	t1,0x87c5f803
beq	t1,s1,hit
nop
la	t1,0x87c40804
beq	t1,s1,hit
nop
la	t1,0x8784080f
bne	t1,s1,end
nop

hit:
la	s1,0x8f04080a
sw	s1,-4(v1)
nop

end:
srl	v0,s1,0x13
andi	v0,0x1c
j	0x80023bbc
.close


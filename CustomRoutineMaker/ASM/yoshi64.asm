.n64
.create "out.bin", 0x80000000


.org	0x800348b4
j	0x803fffdc


.org	0x803fffdc


la     	v1,0x800f7ea0
lbu     v1,0xaf6(v1)

beq	v1,zero,end
nop
lui	v1,0xc100
sw	v1,0x00c8(s0)
end:
sb	r0,0x00f7(v0)
lbu	v1,0x00f6(v0)	
j	0x800348bc
.close

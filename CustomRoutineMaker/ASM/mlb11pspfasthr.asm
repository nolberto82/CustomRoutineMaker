.psp
.create "out.bin", 0x08800000


.org	0x098fddcc
j	0x08801380

add	r0,r0

.org	0x08801380



swc1	f12,0x420(s0)
lui	t0,0x3f80
sw	t0,0x60(s0)

lwc1	f12,0x60(s0)

j	0x098fddd4
.close

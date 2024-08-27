.ps2
.create "out.bin", 0x20000000


.org	0x20a872b0
j	0x200a0000


.org	0x200a0000



li	s0,2
mult	s1,s0,s1
addu	v1,s1,s0



j	0x20a872b8
.close

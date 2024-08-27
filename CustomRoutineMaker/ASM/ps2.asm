.ps2
.create "out.bin", 0x20000000


.org	0x20000000
j	0x200a0000


.org	0x200a0000






j	0x200a0008
.close

.ps2
.create "out.bin", 0x00000000


.org	 0x2012ac44
j	 0x200a0000

//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	 0x200a0000


li	t0,0x40f0
sw	t0,0x44(a2)


j	 0x2012ac4c
.close

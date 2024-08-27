.ps2
.create "out.bin", 0x00000000


.org	 0x20191fe8
j	 0x200a0000

//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	 0x200a0000


lui	t0,0x3f80
sw	t0,0x11c8(a0)

jr	 ra
.close

.nds
.create "out.bin", 0x00000000


.org	0x00a17be8
bl	0x00000000


//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	0x00000000






bx	lr
.close

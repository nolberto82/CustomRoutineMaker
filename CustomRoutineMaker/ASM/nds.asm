.nds
.create "out.bin", 0x00000000


.org	0x02010000
bl	0x02000000


//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	0x02000000






bx	lr
.close

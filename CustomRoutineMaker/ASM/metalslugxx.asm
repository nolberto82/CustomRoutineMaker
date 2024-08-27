.psp
.create "out.bin", 0x00000000


.org	 0x08c56b84
j	 0x08801000

//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	 0x08801000



lw	a3,0x88(a0)
lb	t0,0x08d53080
andi	t0,t0,0x20
beq	t0,r0,end

lui	t0,0x000a

sw	t0,0x10(a3)
end:
j	 0x08c56b84+8
.close

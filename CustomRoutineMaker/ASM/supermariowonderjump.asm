//.swi

.org	0x00414304-0x4000
bl	main

//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	0x02016b00-0x4000
main:



ldrb    w8,[x19,0x5c]
ands	w8,w8,0x10
beq	end

ldrb	w8,[x19,0xa0]
cmp	w8,0x1d
beq	end

str     wzr,[x19,0xa0]


end:
ldrb    w8,[x19,0xa0]

ret


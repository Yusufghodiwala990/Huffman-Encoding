# Huffman-Encoding
Encoding/Decoding text using Huffman trees. In Huffman Encoding, the character least frequent in a piece of text, has the least number of bits assigned to it, thus reducing the overall bits needed to encode a piece of text. A full binary is built from bottom-up and nodes(characters) are placed in a priority queue where 2 least frequent nodes are removed first. Frequencies of the removed nodes are added and placed back into the Huffman tree/Priority queue. Any left child corresponds to "0" while right "1". 


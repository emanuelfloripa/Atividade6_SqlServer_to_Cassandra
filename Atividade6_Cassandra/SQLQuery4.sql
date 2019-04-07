


SELECT * FROM invoice

SELECT COUNT(*) 
FROM (SELECT DISTINCT number FROM invoice) number



SELECT COUNT(*) invoice_item 
FROM invoice_item ii

SELECT COUNT(*) invoice_with_item 
FROM invoice_item ii
JOIN invoice i ON i.number = ii.invoice_id


SELECT * FROM resource_qualification_value
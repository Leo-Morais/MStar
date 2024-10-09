import React from 'react';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';

function ExportarPDFButton() {
    const exportarComoPDF = () => {
        const elemento = document.body; // Captura o corpo da página inteira

        html2canvas(elemento, { scale: 2 }).then(canvas => {
            const imgData = canvas.toDataURL('image/png');

            // Definir o tamanho do PDF em A4 (210mm x 297mm) em pontos (1mm = 2.83465 pontos)
            const pdf = new jsPDF('p', 'mm', 'a4');
            const pageWidth = pdf.internal.pageSize.getWidth();
            const pageHeight = pdf.internal.pageSize.getHeight();
            
            // Converter as dimensões da imagem em mm para o PDF
            const canvasWidth = canvas.width;
            const canvasHeight = canvas.height;
            const imgWidth = pageWidth;
            const imgHeight = (canvasHeight * imgWidth) / canvasWidth;

            let heightLeft = imgHeight;
            let position = 0;

            // Adiciona a imagem gerada ao PDF
            pdf.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);
            heightLeft -= pageHeight;

            // Caso o conteúdo da página seja maior que uma única página A4, adiciona páginas extras
            while (heightLeft >= 0) {
                position = heightLeft - imgHeight;
                pdf.addPage();
                pdf.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);
                heightLeft -= pageHeight;
            }

            // Salva o PDF
            pdf.save('pagina-capturada.pdf');
        });
    };

    return (
        <button onClick={exportarComoPDF} style={{ marginTop: '20px', padding: '10px', fontSize: '16px', cursor: 'pointer' }}>
            Exportar como PDF
        </button>
    );
}

export default ExportarPDFButton;

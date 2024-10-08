import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Input from '../form/Input';
import SubmitButton from '../form/SubmitButton';
import styles from './TipoMercadoriaForm.module.css';

function TipoMercadoriaForm({ btnText }) {
    const [tipo, setTipo] = useState('');
    const [error, setError] = useState(''); // Estado para armazenar mensagens de erro
    const navigate = useNavigate();  // Usando useNavigate em vez de useHistory

    const handleSubmit = (e) => {
        e.preventDefault();
        
        if (!tipo) {
            setError("O tipo de mercadoria não pode estar vazio.");
            return;
        }

        const newTipoMercadoria = {
            tipo: tipo
        };

        fetch("https://localhost:7116/api/v1/TipoMercadoria", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newTipoMercadoria),
        })
        .then((resp) => resp.json())
        .then((data) => {
            console.log('Tipo de Mercadoria cadastrado com sucesso:', data);
            navigate('/tipoMercadoria');  // Usando navigate para redirecionar
        })
        .catch((err) => {
            console.log('Erro ao cadastrar tipo de mercadoria:', err);
            setError("Erro ao cadastrar tipo de mercadoria."); // Define uma mensagem de erro se a requisição falhar
        });
    };

    return (
        <form onSubmit={handleSubmit} className={styles.form}>
            <Input 
                type="text" 
                text="Tipo de Mercadoria" 
                name="tipo" 
                placeholder="Insira o tipo de mercadoria" 
                handleOnChange={(e) => {
                    setTipo(e.target.value);
                    setError(''); // Limpa a mensagem de erro quando o usuário digita
                }}
                value={tipo}
                required // Adiciona a validação HTML para o campo
            />
            {error && <p className={styles.error}>{error}</p>} {/* Exibe a mensagem de erro */}
            <SubmitButton text={btnText} />
        </form>
    );
}

export default TipoMercadoriaForm;

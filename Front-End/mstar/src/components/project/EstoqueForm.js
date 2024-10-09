import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Input from '../form/Input'; // Certifique-se de que este caminho esteja correto
import Select from '../form/Select'; // Importando o Select para as mercadorias
import SubmitButton from '../form/SubmitButton'; // Certifique-se de que este caminho esteja correto
import estoqueService from '../../services/estoqueService'; // Atualize o caminho conforme necessário
import mercadoriaService from '../../services/mercadoriaService'; // Importando o serviço para buscar mercadorias
import styles from './EstoqueForm.module.css'; // Ajuste o nome do arquivo de estilo conforme necessário

const EstoqueForm = ({ btnText }) => {
    const [quantidade, setQuantidade] = useState('');
    const [idMercadoria, setIdMercadoria] = useState('');
    const [mercadorias, setMercadorias] = useState([]);
    const [error, setError] = useState({});
    const navigate = useNavigate();

    useEffect(() => {
        const fetchMercadorias = async () => {
            try {
                const response = await mercadoriaService.getAll();
                setMercadorias(response);
            } catch (err) {
                console.error('Erro ao buscar mercadorias:', err);
            }
        };

        fetchMercadorias();
    }, []);

    const validate = () => {
        const newErrors = {};
        if (!quantidade) newErrors.quantidade = "A quantidade é obrigatória.";
        if (!idMercadoria) newErrors.idMercadoria = "Selecione uma mercadoria.";
        return newErrors;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Validação dos campos
        const newErrors = validate();
        if (Object.keys(newErrors).length > 0) {
            setError(newErrors);
            return;
        }

        // Construindo o objeto DTO para o estoque
        const estoqueDTO = {
            IdMercadoria: Number(idMercadoria), // Usando Number para garantir a conversão correta
            Quantidade: Number(quantidade), // Usando Number para garantir a conversão correta
        };

        try {
            await estoqueService.add(estoqueDTO);

            // Limpar os campos do formulário após o sucesso
            setQuantidade('');
            setIdMercadoria('');

            // Redirecionar para a página de estoque
            navigate('/estoque');
        } catch (err) {
            console.error('Erro ao adicionar estoque:', err);
            // Mensagem de erro mais informativa
            const errorMessage = err.response?.data || "Erro ao adicionar estoque.";
            setError({ submit: errorMessage });
        }
    };

    return (
        <form onSubmit={handleSubmit} className={styles.form}>
            <Input
                type="number"
                text="Quantidade"
                name="quantidade"
                placeholder="Insira a quantidade"
                handleOnChange={(e) => {
                    setQuantidade(e.target.value);
                    setError({}); // Limpa a mensagem de erro quando o usuário digita
                }}
                value={quantidade}
            />
            {error.quantidade && <p className={styles.error}>{error.quantidade}</p>}

            <Select
                text="Mercadoria"
                name="idMercadoria"
                options={mercadorias.map(mercadoria => ({ id: mercadoria.id, name: mercadoria.nome }))}
                handleOnChange={(e) => {
                    setIdMercadoria(e.target.value);
                    setError({});
                }}
                value={idMercadoria}
            />
            {error.idMercadoria && <p className={styles.error}>{error.idMercadoria}</p>}

            {error.submit && <p className={styles.error}>{error.submit}</p>}
            <SubmitButton text={btnText} />
        </form>
    );
};

export default EstoqueForm;

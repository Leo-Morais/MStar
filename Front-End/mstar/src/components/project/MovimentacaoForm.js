import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Input from '../form/Input'; // Certifique-se de que este caminho esteja correto
import Select from '../form/Select'; // Importando o Select para o tipo de movimentação
import SubmitButton from '../form/SubmitButton'; // Certifique-se de que este caminho esteja correto
import movimentacaoService from '../../services/movimentacaoService'; // Atualize o caminho conforme necessário
import mercadoriaService from '../../services/mercadoriaService'; // Importando o serviço para buscar mercadorias
import styles from './MovimentacaoForm.module.css';

function MovimentacaoForm({ btnText }) {
    const [quantidade, setQuantidade] = useState('');
    const [localMovimentacao, setLocalMovimentacao] = useState('');
    const [tipoMovimentacao, setTipoMovimentacao] = useState('');
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
        if (!localMovimentacao) newErrors.localMovimentacao = "O local de movimentação é obrigatório.";
        if (!tipoMovimentacao) newErrors.tipoMovimentacao = "O tipo de movimentação é obrigatório.";
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

        // Construindo o objeto DTO para a movimentação
        const movimentacaoDTO = {
            quantidade: Number(quantidade), // Usando Number para garantir a conversão correta
            localMovimentacao,
            tipoMovimentacao: tipoMovimentacao.toUpperCase(),
            idMercadoria: Number(idMercadoria), // Usando Number para garantir a conversão correta
        };

        try {
            await movimentacaoService.add(movimentacaoDTO);
            
            // Limpar os campos do formulário após o sucesso
            setQuantidade('');
            setLocalMovimentacao('');
            setTipoMovimentacao('');
            setIdMercadoria('');

            // Redirecionar para a página de movimentação
            navigate('/movimentacao');
        } catch (err) {
            console.error('Erro ao adicionar movimentação:', err);
            // Mensagem de erro mais informativa
            const errorMessage = err.response?.data || "Erro ao adicionar movimentação.";
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

            <Input 
                type="text"
                text="Local de Movimentação"
                name="localMovimentacao"
                placeholder="Insira o local de movimentação"
                handleOnChange={(e) => {
                    setLocalMovimentacao(e.target.value);
                    setError({});
                }}
                value={localMovimentacao}
            />
            {error.localMovimentacao && <p className={styles.error}>{error.localMovimentacao}</p>}

            <Select
                text="Tipo de Movimentação"
                name="tipoMovimentacao"
                options={[
                    { id: 'E', name: 'Entrada' },
                    { id: 'S', name: 'Saída' }
                ]}
                handleOnChange={(e) => {
                    setTipoMovimentacao(e.target.value);
                    setError({});
                }}
                value={tipoMovimentacao}
            />
            {error.tipoMovimentacao && <p className={styles.error}>{error.tipoMovimentacao}</p>}

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
}

export default MovimentacaoForm;

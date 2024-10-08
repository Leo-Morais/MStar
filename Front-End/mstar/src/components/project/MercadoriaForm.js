import { useState, useEffect } from 'react'
import Input from '../form/Input'
import Select from '../form/Select'
import SubmitButton from '../form/SubmitButton'
import { useNavigate } from 'react-router-dom'
import styles from './MercadoriaForm.module.css'

function MercadoriaForm({ btnText }) {
    const [tipoMercadorias, setTipoMercadorias] = useState([])
    const [selectedTipoMercadoria, setSelectedTipoMercadoria] = useState('')
    const [nome, setNome] = useState('')
    const [fabricante, setFabricante] = useState('')
    const [descricao, setDescricao] = useState('')
    
    const [errors, setErrors] = useState({}) // Estado para armazenar erros de validação
    const navigate = useNavigate()

    useEffect(() => {
        fetch("https://localhost:7116/api/v1/TipoMercadoria", {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
            },
        })
        .then((resp) => resp.json())
        .then((data) => {
            setTipoMercadorias(data)
        })
        .catch((err) => console.log('Erro ao carregar tipos de mercadoria:', err))
    }, [])

    const validate = () => {
        const newErrors = {}

        if (!nome) newErrors.nome = "O nome é obrigatório."
        if (!fabricante) newErrors.fabricante = "O fabricante é obrigatório."
        if (!descricao) newErrors.descricao = "A descrição é obrigatória."
        if (!selectedTipoMercadoria) newErrors.tipoMercadoria = "Selecione um tipo de mercadoria."

        return newErrors
    }

    const handleSubmit = (e) => {
        e.preventDefault()

        const newErrors = validate()
        if (Object.keys(newErrors).length > 0) {
            setErrors(newErrors) // Define os erros no estado
            return
        }

        const newMercadoria = {
            nome: nome,
            fabricante: fabricante,
            descricao: descricao,
            tipoMercadoriaId: selectedTipoMercadoria
        }

        fetch("https://localhost:7116/api/v1/Mercadoria", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newMercadoria),
        })
        .then((resp) => resp.json())
        .then((data) => {
            console.log('Mercadoria cadastrada com sucesso:', data)
            navigate('/mercadoria')
        })
        .catch((err) => console.log('Erro ao cadastrar mercadoria:', err))
    }

    return (
        <form onSubmit={handleSubmit} className={styles.form}>
            <Input 
                type="text" 
                text="Nome da Mercadoria" 
                name="nome" 
                placeholder="Insira o nome da mercadoria" 
                handleOnChange={(e) => setNome(e.target.value)}
                value={nome}
            />
            {errors.nome && <p className={styles.error}>{errors.nome}</p>}

            <Input 
                type="text" 
                text="Fabricante" 
                name="fabricante" 
                placeholder="Insira o nome do fabricante" 
                handleOnChange={(e) => setFabricante(e.target.value)}
                value={fabricante}
            />
            {errors.fabricante && <p className={styles.error}>{errors.fabricante}</p>}

            <Input 
                type="text" 
                text="Descrição" 
                name="descricao" 
                placeholder="Insira a descrição da mercadoria" 
                handleOnChange={(e) => setDescricao(e.target.value)}
                value={descricao}
            />
            {errors.descricao && <p className={styles.error}>{errors.descricao}</p>}

            <Select 
                text="Tipo de Mercadoria" 
                name="tipoMercadoriaId" 
                options={tipoMercadorias.map(tipo => ({ id: tipo.id, name: tipo.tipo }))} 
                handleOnChange={(e) => setSelectedTipoMercadoria(e.target.value)}
                value={selectedTipoMercadoria}
            />
            {errors.tipoMercadoria && <p className={styles.error}>{errors.tipoMercadoria}</p>}

            <SubmitButton text={btnText} />
        </form>
    )
}

export default MercadoriaForm

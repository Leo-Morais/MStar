import EstoqueForm from '../project/EstoqueForm'; // Importa o componente de formulário de estoque
import styles from './NewProjects.module.css'

function EstoqueCadastro() {
    return (
        <div className={styles.newproject_container}>
            <h1>Cadastro de Estoque</h1>
            <p>Cadastre uma nova entrada no estoque associada a uma mercadoria existente.</p>
            <EstoqueForm btnText="Enviar" /> {/* Passa o texto do botão */}
        </div>
    );
}

export default EstoqueCadastro;

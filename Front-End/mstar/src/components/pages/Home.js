import styles from './Home.module.css'
import LinkButton from '../layout/LinkButton'

function Home(){
    return (
        <section className={styles.home_container}>
            <h1>Bem-vindo ao <span>Cadastro</span></h1>
            <p>Comece a gerenciar seus projetos agora mesmo!</p>
            
            <div className={styles.button_group}>
                <LinkButton to="/tipoMercadoriaCadastro" text="TipoMercadoria"/>
                <LinkButton to="/mercadoriaCadastro" text="Mercadoria"/>
                <LinkButton to="/estoqueCadastro" text="Estoque"/>
                <LinkButton to="/movimentacaoCadastro" text="Movimentação"/>
                <LinkButton to="/movimentacaoGrafico" text="Grafico de Movimentação"/>
            </div>
        </section>
    )
}

export default Home

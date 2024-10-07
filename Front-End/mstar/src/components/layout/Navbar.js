import { Link } from "react-router-dom";
import Container from "./Container";
import styles from './Navbar.module.css';
import logo from '../../img/casa.png';

function Navbar() {
    return (
        <nav className={styles.navbar}>
            <Container>
                <Link to='/'>
                    <img src={logo} alt="logo" className={styles.logo} />
                </Link>
                <ul className={styles.list}>
                    <li className={styles.item}>
                        <Link to='/'>Home</Link>
                    </li>
                    <li className={styles.item}>
                        <Link to='/estoque'>Estoque</Link>
                    </li>
                    <li className={styles.item}>
                        <Link to='/mercadoria'>Mercadoria</Link>
                    </li>
                    <li className={styles.item}>
                        <Link to='/tipoMercadoria'>TipoMercadoria</Link>
                    </li>
                    <li className={styles.item}>
                        <Link to='/movimentacao'>Movimentação</Link>
                    </li>
                </ul>
            </Container>
        </nav>
    );
}

export default Navbar;
